import type { Load } from "@sveltejs/kit";
import { PUBLIC_API_URL } from "$env/static/public"

export const prerender = false;

export const load: Load = async ({ fetch }) => {
  const res = await fetch(`${PUBLIC_API_URL}/recipe`);
  const recipes: RecipeListItemType[] = await res.json();

  return { recipes };
}