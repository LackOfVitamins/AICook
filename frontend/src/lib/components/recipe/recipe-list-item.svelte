<script lang="ts">
  export let recipe: RecipeListItemType;
</script>

<a href="/recipe/{recipe.id}" class="border bg-card rounded-sm">
  <div class="flex flex-col space-y-2 mb-2 w-[180px]">
    <img class="rounded-t-sm" src="{recipe.imageUrl}" alt="{recipe.title}">
    <div class="font-semibold overflow-hidden line-clamp-2 md:text-md px-3">
      <span>{recipe.title}</span>
    </div>
  </div>
</a>
