<script>
	import { Button } from '$lib/components/ui/button';
  import ModeToggle from "$components/mode-toggle.svelte";
  import * as Avatar from "$lib/components/ui/avatar";
</script>

<header class="sticky top-0 z-50 w-full border-b border-border/40 bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
  <div class="container flex justify-between items-center max-w-screen-2xl h-16 sm:h-20">
    <div>
      <a href="/" class="flex items-center space-x-2">
        <span class="font-bold sm:text-xl">
          AICook
        </span>
      </a>
    </div>
    <div>
      <nav class="flex items-center space-x-2">
        <ModeToggle />

        <Avatar.Root>
          <Avatar.Image src="" alt="example" />
          <Avatar.Fallback>EX</Avatar.Fallback>
        </Avatar.Root>
      </nav>
    </div>
  </div>
</header>
