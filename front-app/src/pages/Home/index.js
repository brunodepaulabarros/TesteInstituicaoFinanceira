import React, { useState, useEffect } from 'react';
import { Container } from './styles';
import Api from "../../services/api";
import {
  Button,
  Dialog,
  DialogTitle,
  TextField,
  DialogActions,
  DialogContent,
} from '@material-ui/core';
import NumberFormat from 'react-number-format';

export default function Home() {

  const [error, setError] = useState('');
  const [balance, setBalance] = useState('0');
  const [open, setOpen] = useState(false);
  const [tipoForm, setTipoForm] = useState('');
  const [value, setValue] = useState();
  const [extrato, setExtrato] = useState([]);

  const handleClickOpen = (opr) => {
    setTipoForm(opr);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setValue('')
  };

  const getExtrato = () => {
    Api.get('/Transactions?tipoRequisicao=Extrato')
      .then((response) => {
        if (response.status !== 200) {
          setError('Erro ao realizar requisição: ' + response.problem)
        } else {
          setExtrato(response.data)
          console.log(response)
        }
      })
      .catch((err) => {
        this.setState({ erro: 'Erro ao realizar requisição: ' + err })
      });
  }
  const getBalance = () => {
    Api.get('/Transactions?tipoRequisicao=Saldo')
      .then((response) => {
        if (response.status !== 200) {
          setError('Erro ao realizar requisição: ' + response.problem)
          setBalance('Erro')
        } else {
          setError('')
          setBalance(response.data.value)
        }
      })
      .catch((err) => {
        setError('Erro ao realizar requisição: ' + err)
      });
  }

  const postTransaction = () => {
    var _value = value
    if (value === '' || parseInt(value) === 0) {
      alert('Informe o valor!')
      return
    }
    if (tipoForm === 'Saque' && _value > 0) {
      _value = _value * (-1)
    }
    Api.post('/Transactions', {
      Value: _value.toString(),
      Description: tipoForm.toString()
    }).then((response) => {
      if (response.status !== 200) {
        setError('Erro ao realizar requisição: ' + response.problem)
        setBalance('Erro')
      } else {
        handleClose()
        getBalance()
        getExtrato()
      }
    })
      .catch((err) => {
        setError('Erro ao realizar requisição: ' + err)
      });
  }

  useEffect(() => {
    getBalance();
    getExtrato();
  }, [])

  return (
    <Container>
      <span className="error">{error}</span>
      <br />
      <span>Saldo total em conta: </span>
      <br />
      <NumberFormat
        value={balance}
        displayType={'text'}
        thousandSeparator={true}
        prefix={'R$'}
        decimalSeparator={"."}
        fixedDecimalScale={true}
        decimalScale={2}
        allowNegative={true}
        renderText={(value) => <span className={balance > 0 ? `saldoPositivo` : `saldoNegativo`}>{value}</span>}
      />
      <br />
      <div className={"btnGroup"}>
        <Button variant="outlined" color="primary" onClick={() => handleClickOpen('Deposito')}>
          Deposito
      </Button>
        <Button variant="outlined" color="primary" onClick={() => handleClickOpen('Saque')}>
          Saque
      </Button>
      </div>
      <hr />
      <span>Detalhes</span>
      <br />
      <div style={{ height: '250px', overflowY: 'scroll' }}>
        {extrato.map((a) =>
          <div key={a.id} className={"extratoDiv"}>
            <NumberFormat
              value={a.value}
              displayType={'text'}
              thousandSeparator={true}
              prefix={'R$'}
              decimalSeparator={"."}
              fixedDecimalScale={true}
              decimalScale={2}
              allowNegative={true}
              renderText={(value) => <span className={a.value > 0 ? `saldoPositivo` : `saldoNegativo`}>{value}</span>}
            />
            <span className={"extratoTxt"}>{a.description}</span>
            <span className={"extratoTxt"}>{a.created_at}</span>
          </div>
        )}
      </div>
      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle>{tipoForm}</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Valor"
            type="number"
            fullWidth
            onChange={(value) => setValue(value.target.value)}
            required
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancel
          </Button>
          <Button onClick={postTransaction} color="primary">
            Confirmar
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}
