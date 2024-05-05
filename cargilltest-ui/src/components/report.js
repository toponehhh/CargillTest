import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Table } from 'semantic-ui-react';

export default function Report() {
    const [APIData, setAPIData] = useState([]);
    useEffect(() => {
        axios.get('http://localhost:13001/ReconcillationReport')
            .then((response) => {
                setAPIData(response.data);
            })
    }, []);


    return (
        <div>
            <Table singleLine>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>A/C code</Table.HeaderCell>
                        <Table.HeaderCell>Description</Table.HeaderCell>
                        <Table.HeaderCell>Supplier code</Table.HeaderCell>
                        <Table.HeaderCell>Supplier name</Table.HeaderCell>
                        <Table.HeaderCell>contract no</Table.HeaderCell>
                        <Table.HeaderCell>Due date</Table.HeaderCell>
                        <Table.HeaderCell>AMOUNT IN CTRM (USD)</Table.HeaderCell>
                        <Table.HeaderCell>Amount in JDE</Table.HeaderCell>
                        <Table.HeaderCell>PD Rate</Table.HeaderCell>
                        <Table.HeaderCell>Expected Loss</Table.HeaderCell>
                        <Table.HeaderCell>SF Acct Title</Table.HeaderCell>
                        <Table.HeaderCell>Insurance</Table.HeaderCell>
                        <Table.HeaderCell>Insurance Rate</Table.HeaderCell>
                        <Table.HeaderCell>Insurance limit USD</Table.HeaderCell>
                        <Table.HeaderCell>Net Exposure</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {APIData.map((data) => {
                        return (
                            <Table.Row>
                                <Table.Cell>{data.cpMappingData.abcodeNumber}</Table.Cell>
                                <Table.Cell>{data.arApData.description}</Table.Cell>
                                <Table.Cell>{data.arApData.supplierCode}</Table.Cell>
                                <Table.Cell>{data.arApData.supplierName}</Table.Cell>
                                <Table.Cell>{data.arApData.contractNo}</Table.Cell>
                                <Table.Cell>{(new Date(data.arApData.dueDate)).toLocaleDateString('en-US')}</Table.Cell>
                                <Table.Cell>{data.arApData.amountInCTRM}</Table.Cell>
                                <Table.Cell>{data.arApData.amountInJDE}</Table.Cell>
                                <Table.Cell>{data.cpMappingData.pdRate}</Table.Cell>
                                <Table.Cell>{data.expectedLoss}</Table.Cell>
                                <Table.Cell>{data.cpMappingData.jdecpName}</Table.Cell>
                                <Table.Cell>{data.hasInsurance? 1: 0}</Table.Cell>
                                <Table.Cell>{data.hasInsurance ? data.insuranceData.insuranceRate*100+'%': null}</Table.Cell>
                                <Table.Cell>{data.insuranceLimit}</Table.Cell>
                                <Table.Cell>{data.netExposure}</Table.Cell>
                            </Table.Row>
                        )
                    })}
                </Table.Body>
            </Table>
        </div>
    )
}