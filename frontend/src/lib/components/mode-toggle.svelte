<script lang="ts">
  import { Button } from "$components/ui/button";
  import { Sun, Moon } from "lucide-svelte";

  import { toggleMode } from "mode-watcher";
</script>

<Button on:click={toggleMode} variant="ghost" size="icon">
  <Sun
    class="absolute h-[1.4rem] w-[1.4rem] sm:h-[1.6rem] sm:w-[1.6rem]  rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0"
  />
  <Moon
    class="absolute h-[1.4rem] w-[1.4rem] sm:h-[1.6rem] sm:w-[1.6rem] rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100"
  />
  <span class="sr-only">Toggle theme</span>
</Button>

