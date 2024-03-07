<script lang="ts">
  import { createRender, createTable, Render, Subscribe } from "svelte-headless-table";
  import { readable, writable, type Writable } from "svelte/store";
  import * as Table from "$components/ui/table";
  import DataTableActions from "./data-table-actions.svelte";
  import { UserRole, type User } from "@/types/user";
  import UserCreateDialog from "./user-dialog.svelte";
  import type { Infer, SuperValidated } from "sveltekit-superforms";
  import type { FormSchema } from "./schema";

  export let users: Writable<User[]>;
  export let userForm: SuperValidated<Infer<FormSchema>>;
  export let updateForm: SuperValidated<Infer<FormSchema>>;
  
  const table = createTable(users)
  const columns = table.createColumns([
    table.column({
      accessor: "id",
      header: "ID",
    }),
    table.column({
      accessor: "email",
      header: "Email",
    }),
    table.column({
      accessor: ({role}) => UserRole[role],
      header: "Role",
    }),
    table.column({
      accessor: (user) => user,
      header: "Actions",
      cell: ({ value }) => {
        return createRender(DataTableActions, { user: value, userUpdateForm: updateForm });
      },
    }),
  ]);

  const { headerRows, pageRows, tableAttrs, tableBodyAttrs } =
    table.createViewModel(columns);
</script>

<div>
  <div class="flex items-center py-4">
    <UserCreateDialog data={userForm} />
  </div>

  <div class="rounded-md border w-full">
    <Table.Root {...$tableAttrs}>
      <Table.Header>
        {#each $headerRows as headerRow}
          <Subscribe rowAttrs={headerRow.attrs()}>
            <Table.Row>
              {#each headerRow.cells as cell (cell.id)}
                <Subscribe attrs={cell.attrs()} let:attrs props={cell.props()}>
                  <Table.Head {...attrs}>
                    <Render of={cell.render()} />
                  </Table.Head>
                </Subscribe>
              {/each}
            </Table.Row>
          </Subscribe>
        {/each}
      </Table.Header>
      <Table.Body {...$tableBodyAttrs}>
        {#each $pageRows as row (row.id)}
          <Subscribe rowAttrs={row.attrs()} let:rowAttrs>
            <Table.Row {...rowAttrs}>
              {#each row.cells as cell (cell.id)}
                <Subscribe attrs={cell.attrs()} let:attrs>
                  <Table.Cell {...attrs}>
                    <Render of={cell.render()} />
                  </Table.Cell>
                </Subscribe>
              {/each}
            </Table.Row>
          </Subscribe>
        {/each}
      </Table.Body>
    </Table.Root>
  </div>
</div>