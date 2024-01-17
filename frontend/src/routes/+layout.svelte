<script lang="ts">
	import "../app.pcss";
  import Header from "$components/header.svelte";
  import { ModeWatcher } from "mode-watcher";
</script>

<div class="app">
	<Header />
	<main>
		<slot></slot>
	</main>

	<footer>
	</footer>
	<ModeWatcher />
</div>

<style>
</style>
