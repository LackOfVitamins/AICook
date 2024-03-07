export enum UserRole {
  Admin,
  User
}

export type User = {
  id: string,
  email: string,
  role: UserRole
}