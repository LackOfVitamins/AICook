// See https://kit.svelte.dev/docs/types#app

import type { LocalToastItem } from "@/toastStore";
import type { LoginSession } from "@/types/loginSession";

// for information about these interfaces
declare global {
	namespace App {
		interface PrivateEnv {
			// $env/static/private
			PRIVATE_API_URL: string
		}

		// interface Error {}
		interface Locals {
			loginSession: LoginSession,
			toasts: Array<LocalToastItem>,
			// token: string
		}
		interface PageData {
			session: LoginSession
		}
		// interface PageState {}
		// interface Platform {}
	}
}

export {};



