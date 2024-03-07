export type Recipe = {
  id: number,
  title: string,
  introText: string,
  imageUrl: string,
  steps: RecipeStep[],
  ingredients: RecipeIngredient[]
};

export type RecipeListItem = {
  id: number,
  title: string,
  imageUrl: string
};

export type RecipeIngredient = {
  name: string,
  quantity: string
}

export type RecipeStep = {
  stepNumber: number,
  stepText: string
}

export type RecipeCreatePost = {
  prompt: string
}
