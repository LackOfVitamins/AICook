import type { LayoutServerLoad } from "./$types";
import { formSchema } from "$routes/recipe/create/schema";
import { superValidate } from "sveltekit-superforms/server";

export const load: LayoutServerLoad = async () => {
  return {
    form: await superValidate(formSchema)
  };
};

