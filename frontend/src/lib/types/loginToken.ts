export type LoginToken = {
  id: string,
  useCount: number,
  lastUsed?: string,
  expires: string,
}