﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Protov4.DTO;
using System.Security.Cryptography;

namespace Protov4.DAO
{
    public class ProductoDAO 
    {
        private readonly IMongoCollection<ProductoDTO> prod;
        // Inicializa la conexión a la base de datos MongoDB y obtiene la colección de productos
        public ProductoDAO(IConfiguration configuration)
        {
            var mongo = new DBMongo(configuration);
            prod = mongo.GetDatabase().GetCollection<ProductoDTO>("Productos");
        }
        // Obtiene una lista de productos según el tipo especificado
        public List<ProductoDTO> ObtenerProductos(string tipo)
        {
            var filtro = Builders<ProductoDTO>.Filter.Eq("Tipo", tipo);

            if (tipo == "Procesador")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Gráfica")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Ram")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Placa")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Fuente")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Almacenamiento")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else
            {
                var query = prod.Find(new BsonDocument()).ToListAsync();
                return query.Result;
            }


        }


        // Obtiene detalles de un producto según su ID en formato string
        public List<ProductoDTO> ObtenerSeleccion(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<ProductoDTO>.Filter.Eq(x => x.Id, objectId);
            return prod.Find(filter).ToList();
        }

        //Método para actualizar las existencias de un producto luego de su compra
        public void ActualizarExistencias(string id,int cantidad)
        {
            var objectId = new ObjectId(id);
            var filtro = Builders<ProductoDTO>.Filter.Eq(p => p.Id, objectId);

            var update = Builders<ProductoDTO>.Update.Inc(p => p.Existencia, -cantidad); // Incrementar la existencia en 10 unidades

            var updateResult = prod.UpdateOne(filtro, update);

        }

        //Inserta un Producto
        public void InsertarProducto(string nombre,float precio,string Marca, int existencia,string tipo,string fabricante,string modelo,string velocidad,string Zócalo,string TamañoVRAM,string Interfaz,string TecnologiaRAM,string tamañomemoria,string Almacenamiento,string[] Descripcion)
        {
            var especificaciones = new BsonDocument
            {
                {"Fabricante", fabricante},
                {"Modelo",modelo},
                {"Velocidad", velocidad},
                {"Zócalo",Zócalo }, {"TamañoVRAM",TamañoVRAM}, {"Interfaz",Interfaz},{"Tecnología",TecnologiaRAM}
                ,{"Tamañomemoria",tamañomemoria},{"Almacenamiento",Almacenamiento},{"Descripción", new BsonArray(Descripcion)}
            };
            var ProductoDTO = new BsonDocument
            {
                {"Nombre_Producto",nombre },
                {"Precio", precio},
                {"Marca", Marca},
                {"Existencia",existencia },
                {"Tipo", tipo},
                {"Especificaciones", especificaciones }

            };
        }
    }
}
