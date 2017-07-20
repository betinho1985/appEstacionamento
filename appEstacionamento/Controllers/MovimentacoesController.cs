using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using appEstacionamento.Models;

namespace appEstacionamento.Controllers
{
    public class MovimentacoesController : Controller
    {
        private appEstacionamentoContext db = new appEstacionamentoContext();

        // GET: Movimentacoes
        public ActionResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(db.Movimentacoes.Where(m => m.placa.Contains(searchString)));
            }
                return View(db.Movimentacoes.ToList());
        }

        // GET: Movimentacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacoes movimentacoes = db.Movimentacoes.Find(id);
            if (movimentacoes == null)
            {
                return HttpNotFound();
            }
            return View(movimentacoes);
        }

        // GET: Movimentacoes/Create
        public ActionResult Create()
        {
            Movimentacoes movimento = new Movimentacoes();            
            movimento.horaEntrada = DateTime.Now.TimeOfDay;            
            return View(movimento);
        }

        // POST: Movimentacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,placa,horaEntrada")] Movimentacoes movimentacoes)
        {
            if (ModelState.IsValid)
            {
                movimentacoes.dataEntrada = DateTime.Now;
                movimentacoes.dataSaida = DateTime.Now;

                db.Movimentacoes.Add(movimentacoes);
                db.SaveChanges();
                TempData["sucesso"] = "Entrada efetuada com sucesso";
                return RedirectToAction("Index");
            }

            return View(movimentacoes);
        }

        // GET: Movimentacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacoes movimentacoes = db.Movimentacoes.Find(id);
            
            if (movimentacoes == null)
            {
                return HttpNotFound();
            }
            
            var result = db.Precos.Where(p => movimentacoes.dataEntrada.Date  >= p.dataIncial  && movimentacoes.dataEntrada.Date <= p.dataFinal).First();
            //var result = from p in Precos where p.dataIncial >= movimentacoes.dataEntrada && p.dataFinal <= movimentacoes.dataEntrada;

            if(result == null)
            {
                return RedirectToAction("Index");
            }
            movimentacoes.horaSaida = DateTime.Now.TimeOfDay;
            movimentacoes = calculaValorHora(movimentacoes, result);

            return View(movimentacoes);
        }

        // POST: Movimentacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,placa,dataEntrada,horaEntrada,dataSaida,horaSaida,duracao,tempoCobrado,preco,valorPagar,status")] Movimentacoes movimentacoes)
        {
            if (ModelState.IsValid)
            {
                movimentacoes.status = true;
                db.Entry(movimentacoes).State = EntityState.Modified;
                db.SaveChanges();
                TempData["sucesso"] = "Saída efetuada com sucesso";
                return RedirectToAction("Index");
            }
            return View(movimentacoes);
        }

        // GET: Movimentacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacoes movimentacoes = db.Movimentacoes.Find(id);
            if (movimentacoes == null)
            {
                return HttpNotFound();
            }
            return View(movimentacoes);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimentacoes movimentacoes = db.Movimentacoes.Find(id);
            db.Movimentacoes.Remove(movimentacoes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Movimentacoes calculaValorHora(Movimentacoes movimentacao, Precos precos)
        {
            movimentacao.duracao = movimentacao.horaSaida.Subtract(movimentacao.horaEntrada);
            int tempoCobrado = 0;
            

            //No texto do teste, para periodo menor que 1 hora e maior que 10 minutos cobra o mesmo valor da hora adicional.
            //Soma valor para periodo menor que 1 hora e maior que 10 minutos.
            if (movimentacao.duracao.Hours == 0 && movimentacao.duracao.Minutes >= 10)
            {
                tempoCobrado += 1;
                movimentacao.valorPagar += precos.valorHoraAdicional;
                movimentacao.preco = precos.valorHoraAdicional;
            } else if (movimentacao.duracao.Hours >= 1) {
                movimentacao.valorPagar += precos.valorHora;
                movimentacao.preco = precos.valorHora;
                tempoCobrado += 1;
                if (movimentacao.duracao.Minutes > 10)
                {
                    //repete para calculo das horas adicionais
                    for (int i = 1; i <= movimentacao.duracao.Hours; i++)
                    {
                        tempoCobrado += 1;
                        movimentacao.valorPagar += precos.valorHoraAdicional;
                    }         
                }
            }

            movimentacao.tempoCobrado = tempoCobrado.ToString();
            
            return movimentacao;
        }
    }
}
