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