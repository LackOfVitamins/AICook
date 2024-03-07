<script lang="ts">
  import MoreHorizontal from "lucide-svelte/icons/more-horizontal";
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import { Button } from "$lib/components/ui/button";
  import UserDialog from "./user-dialog.svelte";
  import type { Infer, SuperValidated } from "sveltekit-superforms";
  import { formSchema, type FormSchema } from "./schema";
  import type { User } from "@/types/user";

  export let user: User;
  export let userUpdateForm: SuperValidated<Infer<FormSchema>>;
</script>

<div>
  <!-- <DropdownMenu.Root>
    <DropdownMenu.Trigger asChild let:builder>
      <Button
        variant="ghost"
        builders={[builder]}
        size="icon"
        class="relative h-8 w-8 p-0"
      >
        <span class="sr-only">Open menu</span>
        <MoreHorizontal class="h-4 w-4" />
      </Button>
    </DropdownMenu.Trigger>
    <DropdownMenu.Content>
      <DropdownMenu.Group>
        <DropdownMenu.Label>Actions</DropdownMenu.Label>
      </DropdownMenu.Group>
      <DropdownMenu.Separator />

    </DropdownMenu.Content>
  </DropdownMenu.Root> -->

  <UserDialog data={userUpdateForm} updateMode={true} userToUpdate={user} />
</div>


