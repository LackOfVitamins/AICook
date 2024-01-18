<script lang="ts">
  import { Checkbox } from "$lib/components/ui/checkbox";
  import * as Card from "$lib/components/ui/card";
  import { Label } from "$lib/components/ui/label";
  import slugify from "slugify";

  export let ingredients: RecipeIngredient[]; 
</script>

<Card.Root class="w-full h-fit md:w-2/5">
  <Card.Header>
    <Card.Title>Ingredients</Card.Title>
  </Card.Header>
  <Card.Content>
    <div class="flex flex-col space-y-2">
      {#each ingredients as ingredient}
        <div class="flex flow-row items-center space-x-2">
          <Checkbox id="{slugify(ingredient.name)}" />
          <Label class="cursor-pointer" for="{slugify(ingredient.name)}">
            {ingredient.quantity} {ingredient.name} 
          </Label>
        </div>
      {/each}    
    </div>
  </Card.Content>
</Card.Root>