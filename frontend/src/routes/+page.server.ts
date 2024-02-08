import type { Load } from "@sveltejs/kit";
import { PRIVATE_API_URL } from "$env/static/private"

export const prerender = false;

export const load: Load = async ({ fetch }) => {
  const res = await fetch(`${PRIVATE_API_URL}/recipe`);
  const recipes: RecipeListItemType[] = await res.json();

  return { recipes };
}