import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TopMenuComponent } from './components/top-menu/top-menu.component';
import { BaseComponent } from './components/base/base.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GuestComponent } from './components/guest/guest.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { AuthInterceptor } from './auth.interceptor';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { AdminSideNavComponent } from './components/admin/admin-side-nav/admin-side-nav.component';
import { UsersComponent } from './components/admin/users/users.component';
import { UsersRolesComponent } from './components/admin/users-roles/users-roles.component';
import { EditUserComponent } from './components/admin/edit-user/edit-user.component';
import { ErrorComponent } from './components/error/error.component';
import { ModeratorSideNavComponent } from './components/moderator/moderator-side-nav/moderator-side-nav.component';
import { CategoriesComponent } from './components/moderator/categories/categories/categories.component';
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
import { EditQuizComponent } from './components/moderator/quiz/edit-quiz/edit-quiz.component';
import { ChangePasswordComponent } from './components/user/change-password/change-password.component';
import { UserSideNavComponent } from './components/user/user-side-nav/user-side-nav.component';
import { UserDetailsComponent } from './components/user/user-details/user-details.component';
import { BriefComponent } from './components/user-results/brief/brief.component';
import { DateTransform } from './pipes/date-transform';
import { ResetPasswordInitComponent } from './components/user/reset-password-init/reset-password-init.component';
import { ResetPasswordComponent } from './components/user/reset-password/reset-password.component';
import { QuizResultsComponent } from './components/moderator/quiz/quiz-results/quiz-results.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminPanelComponent,
    TopMenuComponent,
    BaseComponent,
    LoginComponent,
    RegisterComponent,
    GuestComponent,
    QuizezComponent,
    SideNavComponent,
    AdminSideNavComponent,
    UsersComponent,
    UsersRolesComponent,
    EditUserComponent,
    ErrorComponent,
    ModeratorSideNavComponent,
    CategoriesComponent,
    ModeratorComponent,
    TagsComponent,
    EditCategoryComponent,
    AddCategoryComponent,
    AddTagComponent,
    EditTagComponent,
    QuizDetailsComponent,
    QuizRunComponent,
    AddQuizComponent,
    QuizzezComponent,
    AddQuestionsComponent,
    EditQuizComponent,
    ChangePasswordComponent,
    UserSideNavComponent,
    UserDetailsComponent,
    BriefComponent,
    DateTransform,
    ResetPasswordInitComponent,
    ResetPasswordComponent,
    QuizResultsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
