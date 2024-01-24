import { superValidate } from "sveltekit-superforms/client";
import type { LayoutServerLoad } from "./$types";
import { formSchema } from "@/schema/api/recipe/create";

export const load: LayoutServerLoad = async () => {
  return {
    form: await superValidate(formSchema)
  };
};