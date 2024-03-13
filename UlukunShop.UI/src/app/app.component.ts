import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LayoutComponent } from "./admin/layout/layout.component";
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, AdminModule,UiModule]
})
export class AppComponent {
  title = 'UlukunShop.UI';
}
