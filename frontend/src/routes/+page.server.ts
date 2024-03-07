import type { Load } from "@sveltejs/kit";
import { PRIVATE_API_URL } from "$env/static/private"
import type { RecipeListItem } from "@/types/recipe";

export const prerender = false;

export const load: Load = async ({ fetch }) => {
  const res = await fetch(`${PRIVATE_API_URL}/recipe`);
  const recipes: RecipeListItem[] = await res.json();

  return { recipes };
}