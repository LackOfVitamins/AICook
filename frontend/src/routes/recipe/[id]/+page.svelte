<script lang="ts">
  import RecipeIngredientList from "$components/recipe/recipe-ingredient-list.svelte";
  import RecipeStep from "$components/recipe/recipe-step.svelte";
  export let data;
</script>

<section class="flex flex-col md:flex-row items-center space-y-6 md:justify-around text-center pb-4 md:pb-16">
  <h1 class="scroll-m-20 text-4xl sm:text-4xl font-extrabold tracking-tight lg:text-5xl">{data.recipe.title}</h1>
  <img class="rounded-md md:w-1/4 sm:w-1/2 w-full" src="{data.recipe.imageUrl}" alt="{data.recipe.title}">
</section>

<section class="flex flex-col md:flex-row md:space-x-4 space-y-3 md:space-y-0">
  <div class="md:w-2/5 space-y-3">
    <h3 class="scroll-m-20 text-2xl font-semibold tracking-tight">Ingredients</h3>
    <RecipeIngredientList ingredients={data.recipe.ingredients} />
  </div>
  <div class="w-full space-y-3">
    <h3 class="scroll-m-20 text-2xl font-semibold tracking-tight">Steps</h3>
    <div class="flex flex-col space-y-3">
      {#each data.recipe.steps as step}
        <RecipeStep step={step} />
      {/each}
    </div>
  </div>

</section>