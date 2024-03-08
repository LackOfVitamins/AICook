<script lang="ts">
  import Sidebar from "$components/admin/sidebar.svelte";
  
  const items = [
    {
      title:  "Users",
      href: "/admin/user"
    },
    {
      title:  "Login Tokens",
      href: "/admin/token"
    },
  ]
</script>

<div class="hidden space-y-6 p-10 pb-16 md:block">
	<!-- <Separator class="my-6" /> -->
	<div class="flex flex-col space-y-8 lg:flex-row lg:space-x-12 lg:space-y-0">
		<aside class="-mx-4 lg:w-1/5">
			<Sidebar items={items} />
		</aside>
    <slot />
	</div>
</div>