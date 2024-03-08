<script lang="ts">
	import { onMount } from 'svelte';
	import { toast } from 'svelte-sonner';
	import "../app.pcss";
  import Header from "$components/header.svelte";
  import { ModeWatcher } from "mode-watcher";
  import type { LayoutData } from "./$types";
  import { Toaster } from "$components/ui/sonner";
  import { afterUpdate } from "svelte";
  import { renderToasts } from "@/toast";

	export let data: LayoutData;

	onMount(() => {
		renderToasts(data.toasts);
	});
</script>


<div class="app">
	<Header recipeCreateForm={data.form} />
	<main class="container flex flex-col mt-6 mb-10 max-w-screen-2xl">
		<slot />
	</main>

	<footer>
	</footer>

	<ModeWatcher />
	<Toaster richColors expand />
</div>
