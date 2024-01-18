import type { Load } from "@sveltejs/kit";
import { PUBLIC_API_URL } from "$env/static/public"

// since there's no dynamic data here, we can prerender
// it so that it gets served as a static asset in production
export const prerender = true;

export const load: Load = async ({ fetch }) => {
  const res = await fetch(`${PUBLIC_API_URL}/api/recipe`);
  const recipes: RecipeListItemType[] = await res.json();

  return { recipes };
}