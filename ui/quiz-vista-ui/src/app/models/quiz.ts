export interface Quiz {
    id?: string;
    name: string;
    description: string;
    categoryId: number;
    cmsTitleStyle: string;
    isActive: boolean;
    attemptCount:number;
    publicAccess: boolean;
    tagIds: number[];
  }
  