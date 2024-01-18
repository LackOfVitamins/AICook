<script lang="ts">
  import RecipeIngredientList from "$components/recipe/recipe-ingredient-list.svelte";
  import RecipeStep from "$components/recipe/recipe-step.svelte";
  export let data;
</script>

<section class="flex flex-col md:flex-row items-center space-y-6 md:justify-around pb-16">
  <h1 class="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">{data.recipe.title}</h1>
  <img class="rounded-md md:w-1/4 w-1/2" src="{data.recipe.imageUrl}" alt="{data.recipe.title}">
</section>

<section class="flex flex-col md:flex-row md:space-x-4 space-y-3 md:space-y-0">
  <RecipeIngredientList ingredients={data.recipe.ingredients} />

  <div class="flex flex-col space-y-3 w-full">
    {#each data.recipe.steps as step}
      <RecipeStep step={step} />
    {/each}
  </div>
</section>