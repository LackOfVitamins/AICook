<script lang="ts">
  import DataTable from './data-table.svelte';

  export let data;
</script>

<div class="container">
  {#if data.tokens != undefined}
    <DataTable tokens={data.tokens}/>
  {/if}
</div>