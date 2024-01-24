<script lang="ts">
  import { Checkbox } from "$lib/components/ui/checkbox";
  import * as Card from "$lib/components/ui/card";
  import { Label } from "$lib/components/ui/label";
  import slugify from "slugify";

  export let ingredients: RecipeIngredient[]; 
</script>

<Card.Root class="w-full h-fit ">
  <Card.Content class=" pt-6">
    <div class="flex flex-col space-y-2">
      {#each ingredients as ingredient}
        <div class="flex flow-row items-center space-x-2">
          <Checkbox id="{slugify(ingredient.name)}" />
          <Label class="cursor-pointer text-base" for="{slugify(ingredient.name)}">
            <span class="font-bold">{ingredient.quantity}</span>
            <span>{ingredient.name}</span> 
          </Label>
        </div>
      {/each}    
    </div>
  </Card.Content>
</Card.Root>