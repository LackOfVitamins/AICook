import { derived, writable } from "svelte/store"
import { dedupe } from "./dedupe"
import { page } from "$app/stores"

const internal = writable<LoginSession>()

// derived store from page data to provide our session
const external = dedupe(derived(page, $page => $page.data.session))

export const loginSession = derived([internal, external], ([$internal, $external]) => $internal || $external)