using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Models;
using server.Services.TransactionServices.Interface;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionReadDTO>>> GetTransactions()
    {
        var transactions = await _service.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("{publicId:guid}")]
    public async Task<ActionResult<TransactionReadDTO>> GetTransaction(Guid publicId)
    {
        var transaction = await _service.GetTransaction(publicId);
        if (transaction is null)
            return NotFound();

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionReadDTO>> CreateTransaction(TransactionWriteDTO dto)
    {
        var createdTransaction = await _service.CreateTransaction(dto);

        return CreatedAtAction(nameof(GetTransaction), new { publicId = createdTransaction.PublicId }, createdTransaction);
    }

    [HttpPut("{publicId:guid}")]
    public async Task<ActionResult> UpdateTransaction(Guid publicId, TransactionReadDTO dto)
    {
        var updated = await _service.UpdateTransaction(publicId, dto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{publicId:guid}")]
    public async Task<ActionResult> DeleteTransaction(Guid publicId)
    {
        var deleted = await _service.DeleteTransaction(publicId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
