<script lang="ts">
	import { Button, buttonVariants } from '$lib/components/ui/button';
  import * as Dialog from "$lib/components/ui/dialog";
  import * as Form from "$lib/components/ui/form";

  import type { SuperValidated } from 'sveltekit-superforms';
  import { formSchema, type FormSchema } from '$routes/recipe/create/schema';
  import type { FormOptions } from 'formsnap';
  import type { FormResult } from 'sveltekit-superforms/client';
  import type { ActionData } from '../../../routes/recipe/create/$types';

  let dialogOpen: boolean = false;

  const options: FormOptions<FormSchema> = {
    onResult: (event) => {
      const result = event.result as FormResult<ActionData>;
      
      if(result.type == "success") {
        dialogOpen = false;
      }
    }
  };

  export let form: SuperValidated<FormSchema>;
</script>

<Dialog.Root open={dialogOpen} onOpenChange={(state) => dialogOpen = state}>
  <Dialog.Trigger class={buttonVariants({ variant: "outline" })}>
    Add Recipe
  </Dialog.Trigger>
  <Dialog.Content class="sm:max-w-[425px]">
    <Dialog.Header>
      <Dialog.Title>Add Recipe</Dialog.Title>
      <Dialog.Description>
        Enter your recipe idea below. AI will take over from there!
      </Dialog.Description>
    </Dialog.Header>
    <Form.Root {form} {options} schema={formSchema} action='/recipe/create' let:config>
      <Form.Field {config} name="prompt">
        <Form.Item>
          <Form.Label>Recipe Idea</Form.Label>
          <Form.Input />
          <Form.Description>This is your recipe idea.</Form.Description>
          <Form.Validation />
        </Form.Item>
      </Form.Field>
      <Dialog.Footer>
        <Form.Button>Create</Form.Button>
      </Dialog.Footer>
    </Form.Root>
    <!-- <form on:submit|preventDefault={send}>
      <div class="grid gap-4 py-4">
        <div class="grid grid-cols-4 items-center gap-4">
          <Label class="text-right font-bold">Recipe Idea</Label>
          <Input id="prompt" value="" class="col-span-3" />
        </div>
      </div>
      <Dialog.Footer>
        <Button type="submit">Create</Button>
      </Dialog.Footer>
    </form> -->
  </Dialog.Content>
</Dialog.Root>