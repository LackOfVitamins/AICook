import type { Load } from "@sveltejs/kit";
import { PRIVATE_API_URL } from "$env/static/private"

export const prerender = false;

export const load: Load = async ({ fetch, params }) => {
  const res = await fetch(`${PRIVATE_API_URL}/recipe/${params.id}`);
  const recipe: RecipeType = await res.json();

  return { recipe };
}