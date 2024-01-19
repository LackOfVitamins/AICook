<script lang="ts">
	import "../app.pcss";
  import Header from "$components/header.svelte";
  import { ModeWatcher } from "mode-watcher";
</script>

<div class="app">
	<Header />
	<main class="container flex flex-col mt-6 mb-10 max-w-screen-2xl">
		<slot></slot>
	</main>

	<footer>
	</footer>
	<ModeWatcher />
</div>

<style>
</style>
