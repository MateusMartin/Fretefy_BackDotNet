import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Cidade {
    id: string;
    nome: string;
    uf: string;
}

export interface Regiao {
    id: string;
    nome: string;
    ativa: boolean;
    cidades: Cidade[];
}

export interface RegiaoInsertRequest {
    nome: string;
    cidades: string[]; // Lista de GUIDs (string)
}

export interface RegiaoPutRequest {
    id: string;
    nome: string;
    Cidades: string[];
}

@Injectable({
    providedIn: 'root',
})
export class RegiaoService {
    private apiUrl = 'http://localhost:5000/api/';

    constructor(private http: HttpClient) { }

    obterRegioes(): Observable<Regiao[]> {
        const headers = { 'incluirInativas': 'true' };
        return this.http.get<Regiao[]>(this.apiUrl + 'regiao/get', { headers });
    }

    obterCidadesPorNome(nomeParcial: string): Observable<Cidade[]> {
        return this.http.get<Cidade[]>(this.apiUrl + 'cidade', {
            params: { terms: nomeParcial }
        });
    }

    inserirNovaRegiao(request: RegiaoInsertRequest): Observable<any> {
        return this.http.post(this.apiUrl + 'regiao/insert', request);
    }

    atualizarRegiao(request: RegiaoPutRequest): Observable<any> {
        return this.http.put(this.apiUrl + 'regiao/put', request);
    }

    inativarRegiao(regiaoId: string): Observable<any> {
        const headers = new HttpHeaders().set('regiaoId', regiaoId);
        return this.http.delete(this.apiUrl + 'regiao/Delete', { headers });
    }


    ativarRegiao(regiaoId: string): Observable<any> {
        const headers = new HttpHeaders().set('regiaoid', regiaoId);
        console.log(regiaoId);
        return this.http.put(this.apiUrl + 'regiao/Ativar', {}, { headers });
    }

    removerRegiao(regiaoId: string): Observable<any> {
        const headers = new HttpHeaders().set('regiaoId', regiaoId);
        return this.http.delete(this.apiUrl + 'regiao/Remover', { headers });
    }


    baixarExcel(idRegiao?: string, incluirInativas: boolean = false): void {
        const headers = new HttpHeaders()
            .set('idRegiao', idRegiao || '')
            .set('incluirInativas', incluirInativas.toString());

        this.http.get(this.apiUrl + 'regiao/get-excel', {
            headers,
            responseType: 'blob'  // 👈 importante para arquivos
        }).subscribe(blob => {
            const a = document.createElement('a');
            const objectUrl = URL.createObjectURL(blob);
            a.href = objectUrl;
            a.download = `regioes-${new Date().toISOString().replace(/[:.-]/g, '')}.xlsx`;
            a.click();
            URL.revokeObjectURL(objectUrl);
        }, error => {
            console.error('Erro ao baixar o Excel:', error);
        });
    }



}
