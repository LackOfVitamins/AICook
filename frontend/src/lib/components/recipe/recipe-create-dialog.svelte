<script lang="ts">
	import { buttonVariants } from '$components/ui/button';
  import * as Dialog from "$components/ui/dialog";
  import * as Form from "$components/ui/form";
  import { Input } from '$components/ui/input';

  import { superForm, type SuperValidated } from 'sveltekit-superforms';
  import { formSchema, type FormSchema } from '$routes/recipe/create/schema';
  // import type { FormOption } from 'formsnap';
  import type { FormResult, Infer } from 'sveltekit-superforms';
  import type { ActionData } from '../../../routes/recipe/create/$types';
  import { zodClient } from 'sveltekit-superforms/adapters';

  let dialogOpen: boolean = false;

  export let data: SuperValidated<Infer<FormSchema>>;
 
  const form = superForm(data, {
    validators: zodClient(formSchema),
    onResult: (event) => {
      const result = event.result as FormResult<ActionData>;
      
      if(result.type == "success") {
        dialogOpen = false;
      }
    }
  });

  const { form: formData, enhance } = form;
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
    <form method="post" action='/recipe/create' use:enhance>
      <Form.Field {form} name="prompt">
        <Form.Control let:attrs>
          <Form.Label>Recipe Idea</Form.Label>
          <Input {...attrs} bind:value={$formData.prompt} />
        </Form.Control>
        <Form.Description>This is your recipe idea.</Form.Description>
        <Form.FieldErrors />
      </Form.Field>
      <Dialog.Footer>
        <Form.Button>Create</Form.Button>
      </Dialog.Footer>
    </form>
  </Dialog.Content>
</Dialog.Root>