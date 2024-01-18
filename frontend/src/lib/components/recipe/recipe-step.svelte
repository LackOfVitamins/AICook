<script lang="ts">
  import * as Card from "$lib/components/ui/card";
  
  export let step: RecipeStepType
</script>

<Card.Root>
  <Card.Content class="py-3">
    <div class="flex flex-row items-center space-x-4 grow">
      <div class="flex w-[50px] h-[50px] min-w-[50px] border rounded-full  items-center justify-center font-bold text-lg">
        <span>{step.stepNumber}</span>
      </div>
      <div class="">
        <p>{step.stepText}</p>
      </div>
    </div>
  </Card.Content>
</Card.Root>