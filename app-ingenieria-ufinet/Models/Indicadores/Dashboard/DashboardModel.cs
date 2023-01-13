namespace app_ingenieria_ufinet.Models.Indicadores.Dashboard
{
    public class DashboardModel
    {
        public int CantidadEstudiosTotales { get; set; }
        public string NombreIngeniero { get; set; }
        public string NombreKam { get; set; }
        public int CantidadEstudiosIngeniero { get; set; }
        public int CantidadEstudiosKam { get; set; }
        public int SitiosTotales { get; set; }
        public int SitiosConCobertura { get; set; }
        public int SitiosConCoberturaParcial { get; set; }
        public int SitiosSinCobertura { get; set; }
        public int CantidadTiposServicio { get; set; }
        public int AnchoDeBanda { get; set; }
        public List<FactibilidadesPorIngenieroModel> FactibilidadesPorIngeniero { get; set; }
        public List<FactibilidadesClientesModel> RankingClientes { get; set; }
        public List<FactibilidadesPorKamModel> FactibilidadesPorKam { get; set; }
    }
}
