<script lang="ts">
  import { afterUpdate } from "svelte";
  import DataTable from "./data-table.svelte";
  import { writable, type Writable } from "svelte/store";
  import type { User } from "@/types/user";
  export let data;

  const userStore: Writable<User[]> = writable(data.users);

  afterUpdate(() => {
    if(data.users != undefined)
      userStore.set(data.users);
  });
</script>

<div class="container">
  {#if data.users != undefined}
    <DataTable users={userStore} userForm={data.createForm} updateForm={data.updateForm} />
  {/if}
</div>