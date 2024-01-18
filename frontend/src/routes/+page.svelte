<script lang="ts">
  import Recipe from "$components/recipe/recipe-list-item.svelte";
  export let data;
</script>

<svelte:head>
</svelte:head>

<section class=" w-full">
  <div class="flex flex-1 flex-row flex-wrap list-none ml-0 pl-0 gap-4 items-start">
    {#each data.recipes as recipe}
      <Recipe recipe={recipe} />
    {/each}
  </div>
</section>
