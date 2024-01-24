import type { Load } from "@sveltejs/kit";
import { PUBLIC_API_URL } from "$env/static/public"

export const prerender = false;

export const load: Load = async ({ fetch, params }) => {
  const res = await fetch(`${PUBLIC_API_URL}/api/recipe/${params.id}`);
  const recipe: RecipeType = await res.json();

  return { recipe };
}