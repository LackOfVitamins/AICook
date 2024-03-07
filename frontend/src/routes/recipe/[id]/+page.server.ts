import type { Load } from "@sveltejs/kit";
import { PRIVATE_API_URL } from "$env/static/private"
import type { Recipe } from "@/types/recipe";

export const prerender = false;

export const load: Load = async ({ fetch, params }) => {
  const res = await fetch(`${PRIVATE_API_URL}/recipe/${params.id}`);
  const recipe: Recipe = await res.json();

  return { recipe };
}