using Protov4.DTO;

namespace Protov4.DAO
{
    public abstract class MikuTechFactory
    {
        //Insertar nuevo Producto 
        public abstract void InsertarProducto(string nombre, string imagen,float precio, string Marca, int existencia, string tipo, string fabricante, string modelo, string velocidad, string Zócalo, string TamañoVRAM, string Interfaz, string TecnologiaRAM, string tamañomemoria, string Almacenamiento, List<string> Descripcion);

        // Obtiene una lista de todos los productos basados en un tipo
        public abstract List<ProductoDTO> GetAllProductos(string tipo);
        // Obtiene una lista de todos los productos basados en un tipo
        
        public abstract List<AuditoriaDTO> ObtenerAuditoria();
        // Obtiene detalles de un producto específico según su identificador
        public abstract List<ProductoDTO> GetSeleccion(string id);
        // Inserta un nuevo elemento en el carrito de compras
        public abstract void insertarCarrito(int id_pedido, string id_producto,decimal precio,int cantidad,decimal subtotal);
        // Inserta un nuevo detalle de pedido
        public abstract void insertarPedido_Detalle(int id_pedido, string id_producto, decimal precio, int cantidad, decimal subtotal);
        // Obtiene una lista de elementos en el carrito de compras
        public abstract List<PedidoDetalleDTO> ObtenerCarrito();
        // Obtiene una lista de elementos del carrito con detalles completos según un ID de pedido
        public abstract List<CarritoFullDTO> ObtenerCarritoFull(int id);
        // Elimina un producto del carrito de compras
        public abstract void EliminarProductoCarrito(int id, string idproducto);
        // Registra un nuevo pedido para un cliente
        public abstract void RegistrarPedido(int id_cliente);
        // Actualiza los detalles de un pedido en la base de datos
        public abstract void ActualizarPedidoDetalle(string id_producto,int id_pedido, int cantidad, decimal subtotal_producto);
        // Actualiza los detalles generales de un pedido en la base de datos
        public abstract void ActualizarPedido(int id_pedido, decimal pago_total, int tipoestado,string Ciudad_envio, string Calle_principal, string Calle_secundaria, int id_tipo_pago, DateTime fecha_pedido);
        // Obtiene el último ID de pedido registrado
        public abstract int ObtenerIdPedido();
        // Obtiene detalles de un pedido específico según su ID
        public abstract List<PedidoDTO> ObtenerPedidoPorId(int idPedido);
        //Actualiza las existencias de un producto luego de una compra
        public abstract void ActualizarExistencias(string id,int cantidad);
        //Metodo para eliminar un producto de mongo 
        public abstract void eliminarProducto(string id);
    }
}
