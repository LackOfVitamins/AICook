<script lang="ts">
	import { Button, buttonVariants } from '$lib/components/ui/button';
  import * as Dialog from "$lib/components/ui/dialog";
  import * as Form from "$lib/components/ui/form";

  import { PUBLIC_API_URL } from "$env/static/public";
  import type { SuperValidated } from 'sveltekit-superforms';
  import { formSchema, type FormSchema } from '$lib/schema/api/recipe/create';
  import type { FormOptions, SubmitFunction } from 'formsnap';

  let dialogOpen: boolean = false;

  const options: FormOptions<typeof formSchema> = {
    validators: formSchema,
    onSubmit: async ({}) => {
      dialogOpen = !dialogOpen;
    }
  };

  export let form: SuperValidated<FormSchema>;
</script>

<Dialog.Root open={dialogOpen}>
  <Dialog.Trigger class={buttonVariants({ variant: "outline" })}>
    Add Recipe
  </Dialog.Trigger>
  <Dialog.Content class="sm:max-w-[425px]">
    <Dialog.Header>
      <Dialog.Title>Add Recipe</Dialog.Title>
      <Dialog.Description>
        Enter a recipe idea below. AI will take over from there!
      </Dialog.Description>
    </Dialog.Header>
    <Form.Root {form} {options} schema={formSchema} action={`${PUBLIC_API_URL}/api/recipe/create`} let:config >
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