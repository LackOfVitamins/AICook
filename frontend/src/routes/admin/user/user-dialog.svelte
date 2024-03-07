<script lang="ts">
  import * as Dialog from "$lib/components/ui/dialog";
  import * as Form from "$lib/components/ui/form";
  import * as Select from '$components/ui/select';
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import { Button } from '$components/ui/button';
  import { superForm } from 'sveltekit-superforms';

  import type { Infer, SuperValidated, FormResult } from 'sveltekit-superforms';
  import { formSchema, type FormSchema } from './schema';
  import type { ActionData } from './$types';
  import { zodClient } from 'sveltekit-superforms/adapters';
  import Input from '$components/ui/input/input.svelte';
  import { Edit, Plus } from 'lucide-svelte';
  import { UserRole, type User } from '@/types/user';
  import { enumToKeyValuePairs } from '@/utils';
  import { invalidate, invalidateAll } from "$app/navigation";

  let dialogOpen: boolean = false;

  export let data: SuperValidated<Infer<FormSchema>>;
  export let updateMode = false;
  export let userToUpdate: User | undefined = undefined;

  if(userToUpdate != undefined && updateMode) {
    data.data = {
      ...userToUpdate,
      password: null
    };

    data.id = userToUpdate.id;
  }

  const form = superForm(data, {
    validators: zodClient(formSchema),
    onResult: async (event) => {
      const result = event.result as FormResult<ActionData>;
      
      if(result.type == "success") {
        dialogOpen = false;
      }
    }
  });

  const { form: formData, enhance } = form;

  $: selectedRole = $formData.role
    ? {
        label: UserRole[$formData.role],
        value: $formData.role
      }
    : undefined;

</script>

<Dialog.Root open={dialogOpen} onOpenChange={(state) => dialogOpen = state}>
  <Dialog.Trigger asChild let:builder>
    {#if updateMode}
      <Button variant="ghost" size="icon" builders={[builder]}>
        <Edit class="h-5 w-5"/>
      </Button>
    {:else}
      <Button variant="outline" size="icon" class="ml-auto" builders={[builder]}>
        <Plus />
      </Button>
    {/if}
  </Dialog.Trigger>

  <Dialog.Content class="sm:max-w-[425px]">
    <Dialog.Header>
      <Dialog.Title>{ updateMode ? "Update" : "Create" } User</Dialog.Title>
    </Dialog.Header>

    <form method="post" use:enhance>
      <Form.Field {form} name="email">
        <Form.Control let:attrs>
          <Form.Label>Email</Form.Label>
          <Input {...attrs} type="email" bind:value={$formData.email} />
        </Form.Control>
        <Form.FieldErrors />
      </Form.Field>

      <Form.Field {form} name="password">
        <Form.Control let:attrs>
          <Form.Label>Password</Form.Label>
          <Input {...attrs} type="password" bind:value={$formData.password} />
        </Form.Control>
        <Form.FieldErrors />
      </Form.Field>

      <Form.Field {form} name="role">
        <Form.Control let:attrs>
          <Form.Label>Role</Form.Label>
          <Select.Root 
              selected={selectedRole}
              onSelectedChange={(s) => {
                s && ($formData.role = s.value);
              }}
            >
            <Select.Input name={attrs.name} />
            <Select.Trigger {...attrs}>
              <Select.Value placeholder="Select Role" />
            </Select.Trigger>
            <Select.Content >
              {#each enumToKeyValuePairs(UserRole) as {key, value}}
                <Select.Item value={value}>{key}</Select.Item>
              {/each}
            </Select.Content>
          </Select.Root>
        </Form.Control>
        <Form.FieldErrors />
      </Form.Field>

      <Dialog.Footer class="mt-5">
        <Form.Button>{ updateMode ? "Update" : "Create" }</Form.Button>
      </Dialog.Footer>
    </form>
  </Dialog.Content>
</Dialog.Root>