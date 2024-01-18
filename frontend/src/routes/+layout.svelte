<script lang="ts">
	import "../app.pcss";
  import Header from "$components/header.svelte";
  import { ModeWatcher } from "mode-watcher";
</script>

<div class="app">
	<Header />
	<main class="container flex flex-col my-10 max-w-screen-2xl">
		<slot></slot>
	</main>

	<footer>
	</footer>
	<ModeWatcher />
</div>

<style>
</style>
