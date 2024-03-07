import type { User } from "./user"

interface ILoginSession { 
  token: string,
  user: User
}

export type LoginSession = ILoginSession | undefined