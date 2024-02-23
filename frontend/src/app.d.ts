// See https://kit.svelte.dev/docs/types#app
// for information about these interfaces
declare global {
	namespace App {
		interface PrivateEnv {
			// $env/static/private
			PRIVATE_API_URL: string
		}

		// interface Error {}
		interface Locals {
			loginSession: LoginSession
			// token: string
		}
		interface PageData {
			session: LoginSession
		}
		// interface PageState {}
		// interface Platform {}
	}

	type RecipeType = {
		id: number,
		title: string,
		introText: string,
		imageUrl: string,
		steps: RecipeStepType[],
		ingredients: RecipeIngredient[]
	};
	
	type RecipeListItemType = {
		id: number,
		title: string,
		imageUrl: string
	};
	
	type RecipeIngredient = {
		name: string,
		quantity: string
	}
	
	type RecipeStepType = {
		stepNumber: number,
		stepText: string
	}
	
	type RecipeCreatePostType = {
		prompt: string
	}

	enum UserRole {
		Admin,
		User
	}

	type User = {
		id: string,
		email: string,
		role: UserRole
	}

	interface ILoginSession { 
		token: string,
		user: User
	}

	type LoginSession = ILoginSession | undefined
}




export {};



