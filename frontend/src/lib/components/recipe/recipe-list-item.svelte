<script lang="ts">
  export let recipe: RecipeListItemType;
</script>

<a href="/recipe/{recipe.id}" class="border bg-card rounded-sm md:w-[195px] w-[140px]">
  <div class="flex flex-col space-y-2 mb-2  min-h-full">
    <img class="rounded-t-sm" src="{recipe.imageUrl}" alt="{recipe.title}">
    <div class="font-semibold overflow-hidden line-clamp-2 text-sm md:text-base px-3">
      <span>{recipe.title}</span>
    </div>
  </div>
</a>
