import { Routes } from '@angular/router';
import { RegioesComponent } from './regioes/regioes.component';

export const routes: Routes =
    [
        { path: '', component: RegioesComponent },
        {
            path: 'regioes/nova',
            loadComponent: () => import('./pages/regiao-form.component').then(m => m.RegiaoFormComponent)
        },
        {
            path: 'regioes/editar/:id',
            loadComponent: () => import('./pages/regiao-form.component').then(m => m.RegiaoFormComponent)
        }

    ];
