<script lang="ts">
  import Button from "$components/ui/button/button.svelte";
  import { page } from "$app/stores";
  import { cn } from "@/utils";
  import { cubicInOut } from "svelte/easing";
  import { crossfade } from "svelte/transition";

	const [send, receive] = crossfade({
		duration: 250,
		easing: cubicInOut,
	});

  export let items: { href: string; title: string }[];
  let className: string | null | undefined = undefined;

  export { className as class };
</script>

<nav class={cn("flex space-x-2 lg:flex-col lg:space-x-0 lg:space-y-1", className)}>
	{#each items as item}
		{@const isActive = $page.url.pathname === item.href}

		<Button
			href={item.href}
			variant="ghost"
			class={cn(
				!isActive && "hover:underline",
				"relative justify-start hover:bg-transparent text-normal font-semibold"
			)}
			data-sveltekit-noscroll
		>
			{#if isActive}
				<div
					class="absolute inset-0 rounded-md bg-muted"
					in:send={{ key: "active-sidebar-tab" }}
					out:receive={{ key: "active-sidebar-tab" }}
				/>
			{/if}
			<div class="relative">
				{item.title}
			</div>
		</Button>
	{/each}
</nav>

<!-- <div class={cn("pb-12", className)}>
	<div class="space-y-4 py-4">
		<div class="px-3 py-2">
			<div class="space-y-1">
				<Button variant="link" class="w-full justify-start text-normal font-semibold">
          <User class="mr-2 h-5 w-5" />
          Users
				</Button>

        <Button variant="link" class="w-full justify-start text-normal font-semibold">
          <ScanEye class="mr-2 h-5 w-5" />
          Login Tokens
				</Button>
			</div>
		</div>
	</div>
</div> -->