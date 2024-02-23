import type { LayoutServerLoad } from "./$types";
import { formSchema } from "$routes/recipe/create/schema";
import { superValidate } from "sveltekit-superforms/server";

export const load: LayoutServerLoad = async ({ locals }) => {
  const { loginSession } = locals

  return {
    form: await superValidate(formSchema),
    session: loginSession
  };
};

