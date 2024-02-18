import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuestComponent } from './components/guest/guest.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { UsersComponent } from './components/admin/users/users.component';
import { UsersRolesComponent } from './components/admin/users-roles/users-roles.component';
import { EditUserComponent } from './components/admin/edit-user/edit-user.component';
import { AdminGuard } from './services/admin-guard-service';
import { ErrorComponent } from './components/error/error.component';
import { UserGuard } from './services/user-guard-service';
import { CategoriesComponent } from './components/moderator/categories/categories/categories.component';
import { ModeratorGuard } from './services/moderator-guard-service';
import { ModeratorComponent } from './components/moderator/moderator/moderator.component';
import { TagsComponent } from './components/moderator/tags/tags/tags.component';
import { EditCategoryComponent } from './components/moderator/categories/edit-category/edit-category.component';
import { AddCategoryComponent } from './components/moderator/categories/add-category/add-category.component';
import { AddTagComponent } from './components/moderator/tags/add-tag/add-tag.component';
import { EditTagComponent } from './components/moderator/tags/edit-tag/edit-tag.component';
import { QuizDetailsComponent } from './components/quizez/quiz-details/quiz-details.component';
import { QuizRunComponent } from './components/quizez/quiz-run/quiz-run.component';
import { AddQuizComponent } from './components/moderator/quiz/add-quiz/add-quiz.component';
import { QuizzezComponent } from './components/moderator/quiz/quizzez/quizzez.component';
import { AddQuestionsComponent } from './components/moderator/quiz/add-questions/add-questions.component';
import { BriefComponent } from './components/user-results/brief/brief.component';
import { EditQuizComponent } from './components/moderator/quiz/edit-quiz/edit-quiz.component';
import { ChangePasswordComponent } from './components/user/change-password/change-password.component';
import { UserDetailsComponent } from './components/user/user-details/user-details.component';
import { ResetPasswordInitComponent } from './components/user/reset-password-init/reset-password-init.component';
import { ResetPasswordComponent } from './components/user/reset-password/reset-password.component';
import { QuizResultsComponent } from './components/moderator/quiz/quiz-results/quiz-results.component';

const routes: Routes = [
  { path: 'home', component: GuestComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'forgot-password', component: ResetPasswordInitComponent},
  { path: 'reset-password', component: ResetPasswordComponent},

  { path: 'quizez', component: QuizezComponent, canActivate:[UserGuard]},
  { path: 'quizez/category/:categoryName', component: QuizezComponent , canActivate:[UserGuard]},
  { path: 'quizez/tag/:tagName', component: QuizezComponent, canActivate:[UserGuard]},
  { path: 'quiz-details/:quizName', component: QuizDetailsComponent, canActivate:[UserGuard]},
  { path: 'quiz-run/:quizName', component: QuizRunComponent, canActivate:[UserGuard]},
  
  { path: 'quizez', component: QuizezComponent, canActivate:[UserGuard]},
  { path: 'user-details', component: UserDetailsComponent, canActivate:[UserGuard]},
  { path: 'change-password', component: ChangePasswordComponent, canActivate:[UserGuard]},
  { path: 'results', component: BriefComponent, canActivate:[UserGuard]},
  { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] },
  { path: 'admin/users', component: UsersComponent , canActivate: [AdminGuard]},
  { path: 'admin/users-roles', component: UsersRolesComponent, canActivate: [AdminGuard] },
  { path: 'admin/edit-user/:id', component:EditUserComponent, canActivate: [AdminGuard] },
  { path: 'moderator', component:ModeratorComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/categories', component:CategoriesComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-category', component:AddCategoryComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/edit-category/:id', component:EditCategoryComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/tags', component:TagsComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-tag', component:AddTagComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/edit-tag/:id', component:EditTagComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-quiz', component:AddQuizComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/edit-quiz/:quizName', component:EditQuizComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/quiz-results/:quizName', component:QuizResultsComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-questions/:quizName', component:AddQuestionsComponent, canActivate: [ModeratorGuard] },

  
  { path: 'moderator/quizzez', component:QuizzezComponent, canActivate: [ModeratorGuard] },

  { path: 'error/:code', component: ErrorComponent },

  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Domyślna ścieżka
  { path: '**', redirectTo: '/error/404' } // Obsługa nieznanych ścieżek

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
