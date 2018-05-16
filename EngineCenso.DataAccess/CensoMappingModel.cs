using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineCenso.DataAccess
{
    public class CensoMappingModel
    {
        public CensoMappingModel()
        {

        }

        public CensoMappingModel(string name, string cidadesPath, string nomeCidadePath, string habitantesCidadePath, string bairrosPath, string nomeBairroPath, string habitantesBairroPath)
        {
            this.Name = name;
            this.CidadesPath = cidadesPath;
            this.NomeCidadePath = nomeCidadePath;
            this.HabitantesCidadePath = habitantesCidadePath;
            this.BairrosPath = bairrosPath;
            this.NomeBairroPath = nomeBairroPath;
            this.HabitantesBairroPath = habitantesBairroPath;
        }

        [BsonId]
        public ObjectId InternalId { get; set; }
        
        public string Name { get; set; }
        public string CidadesPath { get; set; }
        public string NomeCidadePath { get; set; }
        public string HabitantesCidadePath { get; set; }
        public string BairrosPath { get; set; }
        public string NomeBairroPath { get; set; }
        public string HabitantesBairroPath { get; set; }

        public CensoPropertyMapper ToPropertyMapper()
        {
            return new CensoPropertyMapper(CidadesPath, NomeCidadePath, HabitantesCidadePath, BairrosPath, NomeBairroPath, HabitantesBairroPath);
        }
    }
}
