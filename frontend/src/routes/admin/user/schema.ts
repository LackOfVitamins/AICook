import { UserRole } from "@/types/user";
import { z } from "zod";

export const formSchema = z.object({
  id: z.string().nullable(),
  email: z.string().email(),
  password: z.string().nullable(),
  role: z.nativeEnum(UserRole).default(UserRole.User)
});

export type FormSchema = typeof formSchema;