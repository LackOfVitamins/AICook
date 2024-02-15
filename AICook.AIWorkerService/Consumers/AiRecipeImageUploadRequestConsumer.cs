using AICook.Event.Contracts.Recipe;
using Bytewizer.Backblaze.Client;
using MassTransit;
using RandomString4Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace AICook.AIWorkerService.Consumers;

public class AiRecipeImageUploadRequestConsumer(
    IStorageClient backblazeClient,
    IConfiguration configuration,
    IBus bus, 
    ILogger<AiRecipeImageUploadRequestConsumer> logger)
    : IConsumer<AiRecipeImageUploadRequest>
{
    public async Task Consume(ConsumeContext<AiRecipeImageUploadRequest> context)
    {
        var recipeId = context.Message.RecipeId;
        var imageBytes = context.Message.Image;
        logger.LogInformation("Consuming AiRecipeImageGenerationRequest! Uploading Image for recipe with id: {RecipeId}", recipeId);
        
        using var imageOutputStream = new MemoryStream();
        try
        {
            using var imageInputStream = new MemoryStream(imageBytes);
            using var image = await Image.LoadAsync(imageInputStream);
            
            await image.SaveAsync(imageOutputStream, new WebpEncoder
            {
                FileFormat = WebpFileFormatType.Lossy
            });
        }
        catch (Exception e)
        {
            logger.LogError("Converting image failed! Exception: {Exception}", e);
            return;
        }

        var fileName = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 40) + DateTime.Now.Ticks  + ".webp";
        var bucketName = configuration.GetSection("BackblazeBucket").GetValue<string>("BucketName");
        
        try
        {
            var response = await backblazeClient.ConnectAsync();
            
            var bucket = await backblazeClient.Buckets
                .FindByNameAsync(bucketName);
            
            var uploadResult = await backblazeClient
                .UploadAsync(
                    bucket.BucketId,
                    fileName,
                    imageOutputStream
                );

            uploadResult.EnsureSuccessStatusCode();

            var imageUrl = BuildImageUrl(response.DownloadUrl, bucket.BucketName, fileName);
            
            await bus.Publish(new AiRecipeImageUploaded(recipeId, imageUrl));
        }
        catch (Exception e)
        {
            logger.LogError("Uploading image failed! Exception: {Exception}", e);
        }
    }

    private static string BuildImageUrl(Uri downloadUrl, string bucketName, string filename)
    {
        return $"{downloadUrl.OriginalString}/file/{bucketName}/{filename}";
    }
}