<script lang="ts">
  import { createRender, createTable, Render, Subscribe } from "svelte-headless-table";
  import { readable } from "svelte/store";
  import * as Table from "$components/ui/table";
  import DataTableActions from "./data-table-actions.svelte";
  import { date, string } from "zod";
  import * as DropdownMenu  from "$components/ui/dropdown-menu";
  import { Button } from "$components/ui/button";
  import { Plus } from "lucide-svelte";
  import type { LoginToken } from "@/types/loginToken";

  export let tokens: LoginToken[];

  const table = createTable(readable(tokens))
  const columns = table.createColumns([
    table.column({
      accessor: "id",
      header: "ID",
    }),
    table.column({
      accessor: "useCount",
      header: "Use Count",
    }),
    table.column({
      accessor: ({lastUsed}) => {
        if(lastUsed != undefined)
          return new Date(lastUsed)

        return "Never";
      },
      header: "Last Used",
      cell: ({ value }) => {
        if(value instanceof Date)
        {
            const formatted = new Intl.DateTimeFormat("en-GB", {
              dateStyle: "short",
              timeStyle: "short"
            }).format(value);

          return formatted
        }

        return value;
      },
    }),
    table.column({
      accessor: ({expires}) => {
        return new Date(expires)
      },
      header: "Expires",
      cell: ({ value }) => {
        const formatted = new Intl.DateTimeFormat("en-GB", {
          dateStyle: "short",
          timeStyle: "short"
        }).format(value);
        return formatted;
      },
    }),
    table.column({
      accessor: ({ id }) => id,
      header: "",
      cell: ({ value }) => {
        return createRender(DataTableActions, { id: value });
      },
    }),
  ]);

  const { headerRows, pageRows, tableAttrs, tableBodyAttrs } =
    table.createViewModel(columns);
</script>

<div>
  <div class="flex items-center py-4">
    <DropdownMenu.Root>
      <DropdownMenu.Trigger asChild let:builder>
        <Button variant="outline" size="icon" class="ml-auto" builders={[builder]}>
          <Plus />
        </Button>
      </DropdownMenu.Trigger>
      <DropdownMenu.Content>
      </DropdownMenu.Content>
    </DropdownMenu.Root>
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
