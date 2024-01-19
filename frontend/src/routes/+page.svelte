<script lang="ts">
  import Recipe from "$components/recipe/recipe-list-item.svelte";
  export let data;
</script>

<svelte:head>
  <title>AICook</title>
</svelte:head>

<section class="flex flex-col items-center">
  <div class="space-y-3 w-fit">
    <h3 class="font-bold text-2xl">Recipes</h3>
    <div class="flex flex-1 flex-row flex-wrap list-none ml-0 pl-0 gap-4">
      {#each data.recipes as recipe}
        <Recipe recipe={recipe} />
      {/each}
    </div>
  </div>

</section>
